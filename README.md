Instructions:

Step 1: Install-Package log4net
Step 2: Add the configurations "configSections" and "log4net" in app.config or web.config
  <configSections>
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,Log4net"/>
  </configSections>
  
    <log4net>
    <appender name="Appender1"
    type="log4net.Appender.RollingFileAppender" >
      <file value="../../Logs/AppLogs.log" />      
      <rollingStyle value="Size" />
      <maxSizeRollBackups value="10" />
      <maximumFileSize value="1MB" />
      <staticLogFileName value="true" />
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date %level [%thread] %type.%method - %message%n" />
      </layout>
    </appender>
    <root>
      <level value="All" />
      <appender-ref ref="Appender1" />
    </root>
  </log4net>
  
Step 3: Add this configuration in AssemblyInfo.cs file
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

Visit https://logging.apache.org/log4net/release/manual/configuration.html for more information.