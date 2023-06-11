import serial
import pyautogui

# Modify the serial port name based on your system
serial_port = '/dev/ttyACM0'
baud_rate = 9600

# Open the serial port
ser = serial.Serial(serial_port, baud_rate)

# Wait for the serial connection to be established
ser.timeout = 2
ser.readline()

# Main loop
while True:
    # Read a line of serial data from Arduino
    line = ser.readline().decode().strip()

    # Check if the line is not empty
    if line:
        # Print the received character
        print("Received:", line)

        # Simulate key press
        pyautogui.typewrite(line)

# Close the serial port
ser.close()
