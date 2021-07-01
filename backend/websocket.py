import json

websocket_dict = {}

def connect_room(ws, room_name: str):
    global websocket_dict
    if room_name not in websocket_dict or len(websocket_dict[room_name]) == 0:
        websocket_dict[room_name] = {}
        websocket_id = 1
    else:
        websocket_id = max(websocket_dict[room_name].keys()) + 1
    websocket_dict[room_name][websocket_id] = ws
    while not ws.closed:
        ws.receive()
    websocket_dict[room_name].pop(websocket_id)

def send_to_room(room_name: str, payload) -> bool:
    global websocket_dict
    if room_name not in websocket_dict:
        return False
    for k, ws in websocket_dict[room_name].items():
        ws.send(json.dumps(payload))
    return True
