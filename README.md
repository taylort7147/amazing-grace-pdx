# amazing-grace-pdx

## Overview

Resources for storing, loading, and displaying data for amazinggracepdx.com. 

## Motivation

The motivation for this project was to make the data portable. During the migration from FinalWeb to Nucleus, moving all the audio and videos quickly became unmanageable. We've begun storing the data through third parties like YouTube and Google Drive which was the first step in making the data portable. 

This project furthers that effort by providing a one-stop location for all the links and details for each message, as well as tools for loading and displaying those resources via raw HTML. Most website editors (including Nucleus) offer HTML block elements for building web pages. This project simplifies the loading and displaying of the message details and media in a completely portable way. That way in the future, if we ever decide to switch website providers again, the port will be much less painful - almost automatic.

## What makes the data portable?

Each week is represented as a single entry in our [database](tutorial/description-data.md#Database). The entry contains information about the title, date, description, and links for the audio/video/notes. This data isn't tied to any web editor. The scripts in this project are made to be able to load this data using simple HTML, much like you would embed a YouTube video. This means that any web editor that supports HTML blocks supports this data.

For more details, see [Data](tutorial/description-data.md)

## Tutorials

For tutorials on how to update weekly messages and message series, see the following pages:

[Tutorial: Adding a new series](tutorial/tutorial-adding-a-new-series.md)

[Tutorial: Weekly upload](tutorial/tutorial-weekly-upload.md)

## Programming details

For the more technologically inclined, see how this project is implemented on our website:

* [Website code](tutorial/description-website-code.md)
* [Scripts](tutorial/description-scripts.md) 