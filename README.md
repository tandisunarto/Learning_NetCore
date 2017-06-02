# Learning_NetCore

URL for Hello World DotNet Core Docker 
http://cmelendeztech.com/posts/2017/01/hello-world-dotnet-core-and-docker-with-vs.html

Using Docker Toolbox on Windows with Hyper-V instead of Virtualbox
https://stebet.net/installing-docker-tools-on-windows-using-hyper-v/
First create a Virtual Switch in Hyper-V.
Then, start an Administrative Command Prompt and instead of using the standard documented method to create a Docker VM called default using Virtualbox: docker-machine create --driver virtualbox default
you simply exchange the virtualbox driver with hyperv like so: docker-machine create --driver hyperv default
