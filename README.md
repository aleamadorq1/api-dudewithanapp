# api-dudewithanapp
API To support dudewithanapp.com


Schedule in linux:

Create a file in /etc/systemd/system like this:
[Unit]
Description=Dude with an App API

[Service]
WorkingDirectory=/www/wwwroot/dudewithanapp/api/net7.0
ExecStart=/usr/bin/dotnet /www/wwwroot/dudewithanapp/api/net7.0/DudeWithAnApi.dll 
Restart=always
RestartSec=10
SyslogIdentifier=dudewithanapi
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target


sudo systemctl enable api-dudewithanapp-stage.service 

Stage:
api-dudewithanapp-stage.service 
https://dudewithanapp.site/api/stage

Prod:
api-dudewithanapp.service 
https://dudewithanapp.site/api

Commands:
sudo systemctl status api-dudewithanapp.service 
sudo systemctl restart api-dudewithanapp.service 
