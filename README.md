# okeafurniture
github for our okeafurniture project


INSTRUCTIONS FOR RUNNING API SERVICE
	
	-Ensure you have Docker installed
		- use (docker --version) in powershell to confirm version
		- install docker from https://docs.docker.com/docker-for-windows/install-windows-home/ if not installed
	-Create the docker instance for okea-sql-express using the following command, do not change the password
		docker run `
		--name okea-sql-express `
		-e "ACCEPT_EULA=Y" `
		-e "SA_PASSWORD=YOUR_strong_*pass4w0rd*" `
		-e "MSSQL_PID=Express" `
		-p 1433:1433 `
		-d `
		mcr.microsoft.com/mssql/server:2019-latest

	-run the okea-sql-express using the following command
		docker start okea-sql-express
	-Navigate to Azure Data Studio
		-If you do not have it installed, you can install it from their website
	-Navigate to okeafurniture/SQL Scripts
	-Open okea-db-config in azure studios and run, then close the tab
	-Open GetDailyRevenue in azure studios and run, then close the tab
	-Open GetTopCustomers in azure studios and run, then close the tab
	-Open GetTopItems in azure studios and run, then close the tab

	-Navigate to okeafurniture/okeafurniture.WEB
	-Open the okeafurniture.WEB.sln
	-Run the program
