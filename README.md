# XLoader 2.00
XLoader 2.00 is a Microsoft Windows Tool to flash HEX file onto Arduino/AVR Microcontrollers.

The tool is working on Microsoft Windows Operating System with .Net Framework 4.8.
It has been re-writen by inspired original version made by Geir Lunde http://xloader.russemotto.com

## What's new from v 1.00
 - Use avrdude version 7.2 instead of version 5.4
 - Adding support of ATmega32U4 microcontroller (using bootloader COM Port)
 - Adding uploaded code verification option (avrdude -V option)
 - Adding upload and verification progress information
 - Adding upload and verification logs (avrdude output)
 - Disable Flash erase by default (avrdude -D option)
 - Allows custom avrdude options (to be defined in devices.txt file)
 - Adding Error message if devices.txt or avrdude.exe file is missing
 
## GUI 

![Alt text](./XLoader.gif?raw=true "XLoader 2.00")  
*XLoader Uploading and Verifying Hex file*


![Alt text](./XLoader-Logs.gif?raw=true "XLoader 2.00")  
*XLoader Uploading and Verifying Hex file with log details*

## How to Use
The following steps will help us use XLoader to upload HEX file on ATmega2560 (Arduino Mega).

- Open xLoader
- Browse the HEX File from AVR Project/Atmeal Studio Project
- Select the device E.g. In case of Arduino Mega, it's ATmega2560
- Select COM Port
- Select right baud rate in case of Arduino Mega it's 115200
- Finally Press Upload Button

## Update Device list
Device list information is provided on devices.txt file. 

The default file contains the following AVR MCU configuration : 
- Leonardo [ATmega32U4];atmega32u4;avr109;57600;bootloader;
- Mega [ATmega2560];atmega2560;wiring;115200;default;
- Nano [ATmega328P] (Old Bootloader);atmega328p;arduino;57600;default;
- Nano [ATmega328P];atmega328p;arduino;115200;default;
- Pro Micro [ATmega32U4];atmega32u4;avr109;57600;bootloader;
- Uno [ATmega328];atmega328p;arduino;115200;default;

You can add more devices by editing the file and adding the required information :
- device display name parameter
- partno parameter is the part’s id listed in the avrdude configuration file
- programmer-id parameter is the programmer’s id listed in the avrdude configuration file
- connection baud rate parameter 
- default or bootloader upload mode (bootloader is mandatory for ATmega32U4 MCU)
- [optional] avrdude other custom options (see [`avrdude documentation`](https://avrdudes.github.io/avrdude/7.2/avrdude.html))

parameters must be added with ';' separator character

### Support
XLoader 2.00 has been tested using avrdude 6.3/7.0/7.2. avrdude can be updated if required (avrdude.exe and avrdude.conf files are required)

XLoader 2.00 has been tested with Arduino Mega (ATmega2560); Arduino Nano (ATmega328P); Arduino Leonardo/Micro Pro (ATmega32U4)

## Flashing on Non-Windows Operating Systems
These instructions are provided AS IS. If you encounter difficulty, use the instructions above before reporting an issue.
1. You will need to install the [`avrdude`](https://github.com/avrdudes/avrdude) command.

#### Getting AVRDUDE for Linux

To install AVRDUDE for Linux, install the package `avrdude` by running the following commands:

```console
sudo apt-get install avrdude
```

Alternatively, you may [build AVRDUDE](https://github.com/avrdudes/avrdude/wiki) yourself from source.

#### Getting AVRDUDE for MacOS

On MacOS, AVRDUDE can be installed through Mac Ports.

Alternatively, you may [build AVRDUDE](https://github.com/avrdudes/avrdude/wiki) yourself from source.

#### Using AVRDUDE

AVRDUDE is a command-line application. Run the command `avrdude` without any arguments for a list of options.

A typical command to program your HEX file into your AVR microcontroller looks like this:

```console
avrdude -c <programmer> -p <part> -U flash:w:<file>:i
```

For instance, to program an **Arduino Uno** connected to the serial port **COM1** with a HEX file called `blink.ino.hex`,
you would run the following command:

```console
avrdude -c arduino -P COM1 -b 115200 -p atmega328p -D -U flash:w:objs/blink.ino.hex:i
```