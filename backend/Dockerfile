FROM python:3.9

WORKDIR /app
COPY Pipfile .
COPY Pipfile.lock .
RUN pip install pipenv && \
    pipenv install --system
COPY . .

CMD gunicorn -k geventwebsocket.gunicorn.workers.GeventWebSocketWorker -b 0.0.0.0:5000 server:app
