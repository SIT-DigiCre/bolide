from flask import jsonify, g
from flask_expects_json import expects_json
from websocket import send_to_room

post_comment_schema = {
    'type': 'object',
    'properties': {
        'comment': {
            'type': 'string',
        },
        'is_question': {
            'type': 'boolean',
        },
    },
    'required': [
        'comment',
        'is_question',
    ]
}

@expects_json(post_comment_schema)
def post_comment(room_name: str):
    try:
        is_success = send_to_room(room_name, g.data)
        return jsonify({ 'success': is_success })
    except Exception as e:
        return jsonify({ 'message': str(e) }), 500
