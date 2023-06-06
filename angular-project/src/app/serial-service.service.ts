import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

declare var SerialPort: any; // Declare the SerialPort type

@Injectable({
  providedIn: 'root'
})
export class SerialServiceService { // Updated class name
  private serialPort: any; // Use 'any' type for the serialPort instance
  public receivedData$: Subject<string> = new Subject<string>();

  constructor() {
    this.serialPort = new SerialPort('COM3', { baudRate: 9600 });

    this.serialPort.on('data', (data: Buffer) => {
      this.receivedData$.next(data.toString());
    });
  }

  public send(data: string): void {
    this.serialPort.write(data);
  }
}