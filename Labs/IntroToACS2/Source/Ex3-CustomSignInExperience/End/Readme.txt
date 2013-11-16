In order to be able to run the End solution successfully, you have to go through the following steps:

1. Follow all step in the lab that are performed in the portal (tasks 2 and 3 of the Exercise 1)
    - In Task 3 – Step 10, replacing the following values:
	Realm: https://localhost/WebSiteACSEx3_End/
	Return URL: https://localhost/WebSiteACSEx3_End/Default.aspx
2. Add the STS reference to the the solution, following the task 4 of the Exercise 1.
3. Perform step 18 in task 1 Exercise 3. 
      - Use the following line in the web.config instead:
      <wsFederation passiveRedirectEnabled="true" issuer="https://localhost/WebSiteACSEx3_End/WebSiteACSLoginPageCode.html" realm="https://localhost/WebSiteACSEx3_End/" requireHttps="true"/>   	
4. Open the WebSiteACSLoginPageCode.html file and replace the {yourNamespace} placeholder with the name of your namespace. 