# FLACSharp

This is a simple, low-level binding to `FLAC.dll` / `libFLAC.so` from C# using Platform Invoke.

In order to use it, a platform-appropriate copy of the native FLAC library (`FLAC.dll` on Windows, `libFLAC.so` on Linux / FreeBSD / OS X) will need to be in the loader path or current directory.

## Using

To use this library, add a NuGet package reference to `FLACSharp`. Then, call FLAC library methods as you would from a C/C++ project via the `libFLAC.NativeMethods` class.

The metadata structures make heavy use of unions, which aren't terribly well-supported by Platform Invoke.

* Reading: The marshalling of metadata structures using the `StreamMetadata` class will decode the union bytes into all possible interpretations (the members of `StreamMetadata.Data`). Use the one appropriate to the `Type`.

* Writing: Separate classes matching the layout specific to each type are provided. Use the appropriate type, e.g. `StreamMetadata_with_StreamInfo`, `StreamMetadata_with_Application`. A helper thunk is provided that allows you to supply a `StreamMetadata[]` to `FLAC__stream_encoder_set_metadata` and have the data be marshalled polymorphically.

## Samples

Some simple proof-of-concept sample applications can be found in the `Samples` subdirectory of the library's source code repository (see below).

## Completeness

As of this writing, this library is thrown together extremely hastily in one afternoon. It has been observed to successfully encode and decode FLAC data, but it has not been extensively tested and not all FLAC features have been exercised. If you want to use a feature and it doesn't work properly, open up a Git Issue, or, if you're up for it, code up a fix and make a Pull Request.

## Source Code

The repository for this library's source code is found at:

* https://github.com/logiclrd/FLACSharp/

## License

The library is provided under the MIT Open Source license. See `LICENSE.md` for details.
