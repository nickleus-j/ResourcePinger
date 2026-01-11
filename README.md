# ResourcePinger
 A lightweight tool for pinging websites/URLs and http endpoints.

ResourcePinger is a cross-platform utility built with C#, JavaScript, and Docker that helps developers and sysadmins monitor resource availability. It can be containerized for deployment and extended with custom scripts.

## Installation
`git clone https://github.com/nickleus-j/ResourcePinger.git`

`cd ResourcePinger`

`docker build -t resourcepinger .`

`docker run -p 8080:8080 resourcepinger`

## License
MIT License

