# EventStore.Inspector
Command line tool for searching through and analyzing event streams of EventStoreDB. 

# Features
* Search event streams for
    * any (free) text
    * json attribute/value pairs
    * regular expressions
* List matches in
    * JSON
    * text format
* Throttle ES access by
    * limiting #events to read per batch
    * sleeping period after each batch
    * wait for user interaction after batch

# Usage
```
EventStore.Inspector.exe <Verb> <Options>

Verbs:
  search     Search for events in a stream
  analyse    Analyse stats for a particular stream
  help       Display more information on a specific command.
  version    Display version information.

General options:
  -c (--connection)
            Connection string for the EventStore. (default: tcp://admin:changeit@localhost:1113)
  -v (--verbose)
            Set output to verbose messages
  -s (--stream)
            Event stream to inspect (default: $ce-all)
  --batch   Number of events to process within one batch (default: 100)
  --mode    What to do after processing one batch: AwaitUserInput/Sleep/Continue (default: Sleep)
  --sleep   Number of milliseconds to sleep after each batch (requires Sleep mode, default 500)
  --forward Whether to start processing events from beginning or end of the stream (default: false)
  
Options for "search":
  -t (--text)
            Text to search the event stream for
  -j (--json)
            Specify a JSON property to search for: <key>:<value>
  -r (--regex)
            Specify a regular expression to search for
  -o (--output)
            Output formats: Text/Json (default: text)
  -a (--aggregate)
            Specify how to aggregate multiple search functions: or/and (default: or)
            
Options for "analyse":
  -t (--type)
            Specify a filter for event type (default: empty, any type is allowed)

```
