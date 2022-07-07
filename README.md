# TZInfoGenerator
Used to generate the json time zone data for TimeZonePicker

Generating only the needed data then saving it in JSON format and using it directly in the TimeZonePicker application is much better than depending on the extremely heavy TimeZoneNames package. 

This workaround is better than using TZNames in the blazor app which generated entire seconds of lag and unresponsiveness on initial page load that are unacceptable in such a simple application.
