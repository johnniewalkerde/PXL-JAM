#!/bin/bash
set -e

cd ./.deps/pxl-simulator
npm i
npm run build
npm run dev
