.. distance_tool

---------------------
Distance Matrix Tool
---------------------

Overview
+++++++++
This tool computes the distance between each secondary and its *k* nearest neighbors.



Rationale
++++++++++
A key component of many spatial analysis tasks requires knowing the distance between one or more observations.  Traditionally, this information is either computated while the algorithm runs or stored in a distance matrix.  The former lengths runtimes are the computation is O(n^2) and the later requires that O(n^2) memory be available to store a dense matrix of observations.

We seek to overcome the inherent speed decrease by pre-computing a distance matrix to be used in alter steps.  This allows for iterative parameter exploration without paying an added computational cost with each iteration.  To overcome the memory issues, we store the distance matrix in the `.dbf` table form used by ArcGIS.

