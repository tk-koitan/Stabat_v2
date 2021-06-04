// GENERATED AUTOMATICALLY FROM 'Assets/KoitanLib/InputSystem/KoitanControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @KoitanControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @KoitanControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""KoitanControls"",
    ""maps"": [
        {
            ""name"": ""Platform"",
            ""id"": ""aeb38867-2efa-452e-8a8d-448283139908"",
            ""actions"": [
                {
                    ""name"": ""Stick"",
                    ""type"": ""Value"",
                    ""id"": ""37b6df43-a0bc-4995-8fa6-fbaa6aa895e7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""88cb9443-4c2d-44d9-8399-a53240dacf98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""B"",
                    ""type"": ""Button"",
                    ""id"": ""088a7823-c291-4ce3-bafc-f64466aeb6ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""aa98ae54-c483-4b1c-bc18-55a5da73f392"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Y"",
                    ""type"": ""Button"",
                    ""id"": ""1fb3ba11-5bbc-4e94-9a84-43d09fb7c5b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""e3a8af59-cfa3-4c66-8b28-a90a6b3d817f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""b554a2af-a40d-42cb-b834-3a313210a80c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""89f82fce-0ba6-40e2-b2da-1a7d958d7bd3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""39235c36-bb0d-480c-ac43-b7ada1bcc64d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""7eb89485-f838-4364-8603-b63c1d41c4f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""b0ac428f-b6eb-4c53-b264-ba2ba9b9e170"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrow"",
                    ""id"": ""25ee12ae-c2d6-4468-8b46-ad2963653d86"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""aa88a4c6-c1a5-418d-b146-763b456bd63c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dac06af7-ecf2-4b33-8a7f-6e39346c22ea"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""05337eb8-45a7-44ad-ba2c-2fcb8aaa4b14"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""24adf3a7-1439-4a0f-abfa-3df059678630"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7c73ddcf-2f7d-460a-a8e3-f40b1c1f4443"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e745418e-5c45-45c7-924a-b22c4c6542b4"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/hat"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8d5fb1d-4373-4726-8361-76700d51e28a"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba97d183-9bba-4e0b-99ee-2bc3a22a7071"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0ff46a3-f4ea-422f-8941-44531c76126a"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec37cd2f-5b00-4e46-af32-62e7ae05b752"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c9ddf81-a160-42e3-b7f2-27a47d5774d9"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b00e3d3d-aaf6-43ed-a126-2729afa59233"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21e6ad3e-7ab3-4137-812a-becfb3387aa1"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84bdb697-ad6a-4425-a118-0631263c590c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40c1965d-869a-4f5a-baa8-b80e1ed1a87a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b476cce-4b51-4b6d-8e33-c3264392512f"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e4e0c93-09d7-4dbb-a513-c40e17deefb9"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12ccd60e-a7cf-41c8-9c1d-49e3167b87e8"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""962cf091-c9c0-4305-bdf6-576a314d6fdd"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90e2ef38-9e32-4075-80c3-9ac10ffad05d"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2295e501-4ef5-4747-9c1a-c3ae3984ac8a"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb652a92-11c2-45fa-8645-6be4329bb07b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71a5afb1-33cf-419f-8d95-265c05413f57"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3cdd4c5-9313-4614-b6a0-16a3c0181768"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9056782-3af6-45aa-aca4-cf1e409b4260"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61d62e71-61ee-4b52-815c-e21bf2bec74e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d736c80-a52a-48f4-88e1-f4d960a80138"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66647fcd-b2b4-4646-9d5e-d98211c07c03"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55081c2d-e4dd-42c9-85c6-7582bc5bc116"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c13e13e-8046-4e1c-b8c8-69f9adba525f"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47a16600-5cff-4779-b991-2777c2239a10"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ebdb256-80e6-49e9-915d-c1b7d25a9192"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da1d6f15-ddb0-44ae-a4d2-f21c9120b657"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72c6f393-2b5e-4ac8-93d3-4b972f02a613"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/hat/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe59f5af-5943-4c83-b8e4-24f8139acb07"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acebe3e7-0130-4d8b-b117-187abd8605ed"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7457999b-4f5c-4382-9926-fc6dde8c2764"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acd74949-cd9c-4ec2-9d4c-1b19992b8e22"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed915224-080e-4187-ae51-13547f565c6b"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cccdb61-4bce-4d69-a494-87c5edb582a3"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/hat/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d236844-d6ea-4aa7-874e-c67c75ec5ece"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ed81e9b-d91d-4e94-834a-a8da26e101ed"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""033b39cd-3d5a-4962-b333-d05f72f6936e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b7b160e-8909-4631-ad4c-93fdbf4b8418"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0249a80-dbed-4c30-8fff-4a3322e5fa8e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a14a775e-26cc-41c9-8b8b-d39c304d9671"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/hat/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb536704-15d3-42e7-9913-36f4db595995"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""613dfbb3-def8-4182-9cb7-3e397fead675"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ade767a-fef5-430c-8d7b-5dbc00ac6be5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc75b8a9-1150-4992-9b14-c2e214386d8a"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1762196a-d5f3-435b-a98f-1a29d135585c"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7442c2c5-bb1f-4f57-b8cb-d1a2f86f9b41"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4be479fa-a22e-4625-b0e8-5cd865174522"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6bf3acd-529f-4332-8dde-1e04131fe05f"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8988c544-5164-4444-a005-0b1d732f280d"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fa2b797-2efd-49d0-9673-6dcc0b4abfa7"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bda16e61-d838-4ad4-abd8-dccc3e636d33"",
                    ""path"": ""<HID::HORI CO.,LTD  PAD A>/button9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b856267-5a68-447d-9818-730e9988f12b"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e25c6b1-1522-4893-b73e-7d07b9b80ecc"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Platform
        m_Platform = asset.FindActionMap("Platform", throwIfNotFound: true);
        m_Platform_Stick = m_Platform.FindAction("Stick", throwIfNotFound: true);
        m_Platform_A = m_Platform.FindAction("A", throwIfNotFound: true);
        m_Platform_B = m_Platform.FindAction("B", throwIfNotFound: true);
        m_Platform_X = m_Platform.FindAction("X", throwIfNotFound: true);
        m_Platform_Y = m_Platform.FindAction("Y", throwIfNotFound: true);
        m_Platform_Start = m_Platform.FindAction("Start", throwIfNotFound: true);
        m_Platform_Select = m_Platform.FindAction("Select", throwIfNotFound: true);
        m_Platform_Up = m_Platform.FindAction("Up", throwIfNotFound: true);
        m_Platform_Down = m_Platform.FindAction("Down", throwIfNotFound: true);
        m_Platform_Right = m_Platform.FindAction("Right", throwIfNotFound: true);
        m_Platform_Left = m_Platform.FindAction("Left", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Platform
    private readonly InputActionMap m_Platform;
    private IPlatformActions m_PlatformActionsCallbackInterface;
    private readonly InputAction m_Platform_Stick;
    private readonly InputAction m_Platform_A;
    private readonly InputAction m_Platform_B;
    private readonly InputAction m_Platform_X;
    private readonly InputAction m_Platform_Y;
    private readonly InputAction m_Platform_Start;
    private readonly InputAction m_Platform_Select;
    private readonly InputAction m_Platform_Up;
    private readonly InputAction m_Platform_Down;
    private readonly InputAction m_Platform_Right;
    private readonly InputAction m_Platform_Left;
    public struct PlatformActions
    {
        private @KoitanControls m_Wrapper;
        public PlatformActions(@KoitanControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Stick => m_Wrapper.m_Platform_Stick;
        public InputAction @A => m_Wrapper.m_Platform_A;
        public InputAction @B => m_Wrapper.m_Platform_B;
        public InputAction @X => m_Wrapper.m_Platform_X;
        public InputAction @Y => m_Wrapper.m_Platform_Y;
        public InputAction @Start => m_Wrapper.m_Platform_Start;
        public InputAction @Select => m_Wrapper.m_Platform_Select;
        public InputAction @Up => m_Wrapper.m_Platform_Up;
        public InputAction @Down => m_Wrapper.m_Platform_Down;
        public InputAction @Right => m_Wrapper.m_Platform_Right;
        public InputAction @Left => m_Wrapper.m_Platform_Left;
        public InputActionMap Get() { return m_Wrapper.m_Platform; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlatformActions set) { return set.Get(); }
        public void SetCallbacks(IPlatformActions instance)
        {
            if (m_Wrapper.m_PlatformActionsCallbackInterface != null)
            {
                @Stick.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnStick;
                @Stick.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnStick;
                @Stick.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnStick;
                @A.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnA;
                @B.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnB;
                @B.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnB;
                @B.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnB;
                @X.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnX;
                @Y.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnY;
                @Y.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnY;
                @Y.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnY;
                @Start.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnStart;
                @Select.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnSelect;
                @Up.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnDown;
                @Right.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnRight;
                @Left.started -= m_Wrapper.m_PlatformActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_PlatformActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_PlatformActionsCallbackInterface.OnLeft;
            }
            m_Wrapper.m_PlatformActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Stick.started += instance.OnStick;
                @Stick.performed += instance.OnStick;
                @Stick.canceled += instance.OnStick;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @B.started += instance.OnB;
                @B.performed += instance.OnB;
                @B.canceled += instance.OnB;
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @Y.started += instance.OnY;
                @Y.performed += instance.OnY;
                @Y.canceled += instance.OnY;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
            }
        }
    }
    public PlatformActions @Platform => new PlatformActions(this);
    public interface IPlatformActions
    {
        void OnStick(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnB(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnY(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
    }
}
