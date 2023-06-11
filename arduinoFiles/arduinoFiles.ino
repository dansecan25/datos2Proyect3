const int buttonPin1 = 2;  // Button 1 pin
const int buttonPin2 = 3;  // Button 2 pin
bool buttonState1 = false; // Flag for button 1 state
bool buttonState2 = false; // Flag for button 2 state

void setup() {
  pinMode(buttonPin1, INPUT_PULLUP);
  pinMode(buttonPin2, INPUT_PULLUP);
  Serial.begin(9600);
}

void loop() {
  bool newButtonState1 = digitalRead(buttonPin1) == LOW;
  bool newButtonState2 = digitalRead(buttonPin2) == LOW;

  if (newButtonState1 != buttonState1 && newButtonState2 == buttonState2) {
    buttonState1 = newButtonState1;
    if (buttonState1) {
      Serial.println("1");
    }
  }

  if (newButtonState2 != buttonState2 && newButtonState1 == buttonState1) {
    buttonState2 = newButtonState2;
    if (buttonState2) {
      Serial.println("0");
    }
  }
  
  if (Serial.available()) {
    char receivedChar = Serial.read();
    // Handle any received characters from the computer if necessary
    // For example, you can perform certain actions based on the received character
  }

  delay(100);  // Delay to avoid rapid repeating
}
