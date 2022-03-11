sudo killall -9 dotnet
nohup dotnet /home/ec2-user/app/publish/Admin.API.dll &>/dev/null &
