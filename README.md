This repo contains the code which was used in the IBM project SENSIT under the Civil Engineering Department.  

Folder Server contains the C# solution SENSITServer, which in turn contains the following projects:  

1. Project SENSITServer is the main project of the solution and will be deployed in the final application.
   It serves as the central server for communication with all the sensors in the SENSIT network.
   It logs the data received and also passes it on to another application that makes use of this
   data to predict flood and drainage system performance.  

2. Project SensorEmulator as the name suggests is an emulator for a sensor in the SENSIT network.
   It is basically used to test the SensitServer without requiring the actual sensor to be present
   for communication. This will save a lot of testing time.  

3. Project SENSITServerInstaller creates an setup file for the SENSITServer project that can then
   be distributed to other machines for the purpose of installing SENSITServer. 

4. Project SPortTest for debugging serial port communication.  

Folder Sensor contains the AVRStudio solution Server. It contains only 1 project:  
1. Project Sensor has the code that will be dumped in the AVR microcontrollers that will be installed one on each sensor.  
