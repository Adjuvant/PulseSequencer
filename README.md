# PulseSequencer

A pulse-based sequencer for Unity3d adapted from [Derelict Computer's](http://derelict.computer) original work.

## Info

The PulseSequencer allows composers to create 3d, polyrhythmic music using Unity's built-in audio system. It also allows for triggering visual effects, such as animations or light lerps in sync with the music.

Just clone or fork this repo and import it into your project.

To get started, check out the example scenes.

## Dependency

Import paid version of [Entitas from Unity Asset Store](https://assetstore.unity.com/packages/templates/systems/entitas-87638). 
Free version *may* work.

## Todo

- [x] Add [Native Volume Envelopes](https://github.com/derelictcomputer/UnityVolumeEnvelopeNative)
- [x] Transition to Entitas
- [ ] Add spatial positioning - set all voices in one action?
- [x] Add per step time offset
- [x] Add per step pitch variation
- [x] Add per step gain setting
- [ ] Create a automated creation routine to instantiate from Prefab or Command.
- [ ] Better placement of new content
- [ ] Add controls for pitch, time, gain etc.
- [ ] Add sample offset back in, change time offset to "Swing"
- [ ] Add multiple pulse capability.

## Architecture notes

+ Pass the triggered steps as audioEntities then volume, offset and pitch are extracted when required.
+ Still using voices to improve efficiency
+ Use AbstractListenerBehaviour to register normal game objects to entities.
+ Use Entitas events to throw stuff around, requires classes using component interfaces.
+ 

# Issues
+ Native envelopes won't trigger at high speeds, dependant on num of voices though.

## Platform Support
- Windows: verified working
- Mac: verified working
- Linux: theoretically works, but not verified
- iOS: verified working
- Android: theoretically works, but not verified
- HTML5/WebGL Preview: basic sampling works, but volume envelopes do not (pending support for AudioFilters, ETA from Unity unknown)

## Disclaimer

This project is very young, and therefore many safeguards and editor helpers are not implemented. I'm using this in my own projects, and will be updating this repository as I add features in those projects. So please bear with me! If you have any feature requests or find any bugs, please feel free to report them via the issue tracker.

## Wanna help?

If you like using the Pulse Sequencer, and want to help me develop it, that's awesome! There are a few ways you can help:
- Submit bugs and feature requests via the issue tracker. This will help me know what people want to see, and prioritize those things.
- If you're a coder, fork the repo and submit pull requests for features and bug fixes. This gets a lot better if I have help.
- Let me know how you're using it. Seeing your work inspires me to keep going further, and often gives me ideas for new features.
- If you're a dev studio that wants a novel interactive music solution, [hire me](http://derelict.computer/consulting.html)!
