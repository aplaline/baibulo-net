#!/bin/sh

nuget restore
msbuild /property:Configuration=Release
