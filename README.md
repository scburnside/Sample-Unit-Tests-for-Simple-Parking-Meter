# Sample-Unit-Tests-for-Simple-Parking-Meter
## Sample Unit Tests for Simple Parking Meter

This sample project contains a set of unit tests for a simple Parking Meter.  This was an opportunity to practice NUnit and Moq.  

### Parking Meter Specs:
- A user can add money to the meter which will add more time.
- The user and attendant can see how much remaining time is left.
- The user and attendant can see if the meter is expired.
- The attendant can see if the meters till is full.
- The attendant can see the capacity of the till and how much is in it.
- The attendant can empty the till.

### Project 1:
- ParkingMeter.cs
- ParkingMeterUnitTests.cs 

The first version of the parking meter, ParkingMeter.cs, contains a simple Parking Meter class. I found that some of the code wasn't testable as it was written because it exhibited non-deterministic behavior.  That is, the DateTime.Now was used to increment that time for the parking meter, but it isn't feasable to wait for 15 minutes for each test.  

### Project 2:
- ParkingMeterRefactored.cs
- DateTimeProvider.cs
- MeterTill.cs
- MeterTillUnitTests.cs
- ParkingMeterUnitTestsRefactored.cs

I refactored the code to be more testable by implementing inversion of control and dependency injection with Moq.  DateTime is now "faked" with a return value suitable for testing. I refactored and added unit tests. 
