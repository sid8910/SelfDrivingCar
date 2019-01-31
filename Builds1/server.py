#real-time server
import socketio
#concurrent networking 
import eventlet
#web server gateway interface
import eventlet.wsgi
#web framework
from flask import Flask


sio = socketio.Server()
app = Flask(__name__)

@sio.on('connect')
def connect(sid, environ):
	print("connect ", sid)
	sio.emit(
		"move",
		data={
			'torque': "1.0",
			'steer_angle': "0.0",
		},
		skip_sid=True
	)


@sio.on('telemetry')
def telemetry(sid, data):
	if data:
		sio.emit(
			"move",
			data={
				'torque': "1.0",
				'steer_angle': "0.2",
			},
			skip_sid=True
		)

app = socketio.Middleware(sio, app)
eventlet.wsgi.server(eventlet.listen(('', 4567)), app)
