# Memory leak

This sample demonstrates memory leaks, both managed and native. Common sources of memory leaks include static variables, native calls that don't free memory, or long-lived threads with patterns that root lots of memory (unbound caches, event subscriptions, or objects captured in anonymous methods, for example).

This sample includes a long-lived, poorly implemented cache to demonstrate a managed memory leak and an incorrectly disposed native helper to demonstrate a native memory leak.
