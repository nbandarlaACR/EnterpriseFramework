# **EnterpriseFrameworkCsharp**
Test Automation framework using C# and Selenium

## **How to Clone Framework**

```
git clone https://github.com/nbandarlaACR/EnterpriseFrameworkCsharp.git
```
## **How to Rename Framework Floders**
1. Rename .csproj name to the desired name 
   - Example : ProjectFramework.csproj to ABCProject.csproj
2. Rename Project Folder name to a new desired name
   - Example : ProjectFramework to ABCProject
3. Open EnterPriseFramework.sln with notepad and change the relevant project names with new desired project name
   - Please find below screenshots for reference to  replace ProjectFramework occcurances with new name

###### **From**
![This is an image](/images/old_proj_name.png)

###### **To**
![This is an image](/images/new_proj_name.png)

## **How Add Browser Configurations**
1. Open Solution Configurations and select Configuration Manager
2. Add New Configurtions in ProjectFramework Configuration dropdown
3. Add New Configurations in the Active Solution Configuration

![This is an image](/images/configurations.png)

## **How to Run the script**
1. Provide Application URL in the appsettings.Chrome_Preprod.json
2. Select the Chrome_Preprod from the solution Explorer
3. Open Test Explorer
4. Select the Test case
5. Run

## **How to view Test Results**
> When the test execution is completed to view the test results, perform the below steps in the terminal window

```
cd <ProjectFrameworkDirectory>/TestResults

livingdoc test-assembly <ProjectFrameworkDirectory>\bin\Chrome_Preprod\netcoreapp.3.1\<ProjectName>.dll -t <ProjectFrameworkDirectory>\bin\Chrome_Preprod\netcoreapp.3.1\TestExecution.json
```

###### Example
```
cd C:\SourceCode\main_repo\EnterpriseFrameworkCsharp\ProjectFramework\TestResults

livingdoc test-assembly C:\SourceCode\main_repo\EnterpriseFrameworkCsharp\ProjectFramework\bin\Chrome_Preprod\netcoreapp3.1\ProjectFramework.dll -t C:\SourceCode\main_repo\EnterpriseFrameworkCsharp\ProjectFramework\bin\Chrome_Preprod\netcoreapp3.1\TestExecution.json
```
> Test Results will be generated with failed screenshots under the below path with the name Livindoc.html
```
  <ProjectFrameworkDirectory>/TestResults
```


###### Example Reports
![Example Reports](/images/example-reports.png)