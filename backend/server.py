#!/usr/bin/env python3
from flask import Flask
from flask_sockets import Sockets
from flask_cors import CORS
import comment
import websocket

app = Flask(__name__)
sockets = Sockets(app)
cors = CORS(app)

app.route('/v1/comment/<room_name>', methods=['POST'])(comment.post_comment)

sockets.route('/v1/room/<room_name>')(websocket.connect_room)


if __name__ == "__main__":
    from gevent import pywsgi
    from geventwebsocket.handler import WebSocketHandler
    server = pywsgi.WSGIServer(('', 5000), app, handler_class=WebSocketHandler)
    server.serve_forever()
