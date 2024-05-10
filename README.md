# Unity-iOS-Canvas-Scaler
Easily implement UI designs in Unity with 1:1 replication and constant physical runtime size of UI elements across devices.


# Prerequisites:
From Figma to Unity:
* You should know the frame size of the Figma design and which device the design is created for. For example, the design may be for iPhone 8. iPhone 8 has a logical resolution of 375x667 and the frame size in Figma should match this.
* You should also know the scale factor of the device. iPhone 8 has a scale factor of 2. In Figma you will design using points (logical resolution), but the UI elements need to be exported using the scale factor (@2x). This ensures that the exported resolution of UI elements match the screen's native resolution (750x1334).
* Lastly you need to know the DPI of the device, which in this case is 326. This is needed in Unity to ensure constant physical sizes of the UI elements across devices, and to configure the uGUI Canvas to match the Figma frame.

# Usage:
1. Make sure your build platform is set to either iOS or Android.
2. Copy the iOSCanvasScaler.cs script into your Assets folder.
3. Create a Canvas in your scene (GameObject -> UI -> Canvas) and replace the uGUI "Canvas Scaler" component with an "iOS Canvas Scaler" component.
<img width="530" alt="Screenshot 2024-05-10 at 13 11 16" src="https://github.com/joensaar/Unity-iOS-Canvas-Scaler/assets/43815747/451c0dc6-3696-41ed-b914-e1298cca03f2">

4. Find the DPI of the screen you have designed for from https://www.ios-resolution.com. In the case of iPhone 8, the DPI will be 326 - set the Refrence DPI to this value.
<img width="533" alt="Screenshot 2024-05-10 at 13 52 09" src="https://github.com/joensaar/Unity-iOS-Canvas-Scaler/assets/43815747/a4513dbe-946b-4cc0-b563-05d1f4fe8e71">

6. Make sure you have the Device Simulator active instead of the normal Game window. This script will not work correctly using the normal Game window. Select iPhone 8, since in this example the UI is designed for that device in Figma.
<img width="296" alt="Screenshot 2024-05-10 at 14 03 48" src="https://github.com/joensaar/Unity-iOS-Canvas-Scaler/assets/43815747/f4c52343-58bd-4234-82d4-3d1f87fee8ff">

7. Now check the Canvas's Rect Transform. The Width and Height should now match the logical resolution of the device, and the frame size in Figma (375x667). Also the Scale should be 2, matching the scale factor of the device. Great - now you can be sure that one unit in Figma will be one unit in the uGUI Canvas!
<img width="534" alt="Screenshot 2024-05-10 at 14 10 19" src="https://github.com/joensaar/Unity-iOS-Canvas-Scaler/assets/43815747/d8eb9bd4-51d7-4567-9f31-efade7d17983">

8. Choose a Fallback DPI of your liking - this is important as in some rare cases a device may fail to report a DPI. 326 is the most common DPI on iOS devices. Enable Simulate Fallback DPI and test different devices in the Device Simulator to see what the design would look like using the Fallback DPI.

9. If your Sprites are exported @2x like the should be, and the Sprite's Pixel Per Unit value is 100, you can set the Reference Pixels Per Unit to 100 and the Default Sprite DPI to that of the device's screen: 326. Now you can conveniently use the "Set Native Size" button on Image components, and the size of the Image will be the same as the size in Figma. The physical resolution of the Image is still 2x and will render at native resolution.
<img width="533" alt="Screenshot 2024-05-10 at 14 30 28" src="https://github.com/joensaar/Unity-iOS-Canvas-Scaler/assets/43815747/37ddcf3c-72e8-4c71-91f8-99650769e28e">

10. Finally, you can test your design on any device with Device Simulator. iOS Canvas Scaler ensures that your design maintains the same physical size on any device and orientation! In most cases, the Canvas's Rect Transform's Width, Height, and Scale values will change as different devices are tested.
    
NOTE: In some rare cases, a device in Device Simulator may report an incorrect DPI value which iOS Canvas Scaler relies on in the editor, and may result in UI elements having a larger or smaller scale compared to on the actual device. Check that the Canvas's Rect Transform's values match the values at https://www.ios-resolution.com once iOS Canvas Scaler is configured.
