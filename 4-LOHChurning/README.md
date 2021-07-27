# Frequent LOH usage

This sample demonstrates performance problems with frequent large object heap allocation and collection. Both allocating and cleaning up large objects are slow operations, so it's important to not allocate short-lived large objects on hot code paths. Instead, array pools and other caching techniques can re-use memory for these objects. This sample is called LOH churning because in some discussions, frequently allocating and then releasing/collecting objects (high allocation rate with low survivability) is referred to as churning.

This sample includes frequent allocations of short-lived large objects to demonstrate the performance implications of frequent LOH allocations and clean up.
