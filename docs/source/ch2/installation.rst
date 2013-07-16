.. installation

------------
Installation
------------

Installation Requirements
-------------------------
This tool has been designed to function with ArcMap version 10.0 and greater.  It is not possible to run the tool with versions less than 10.0.  Please download the old version of the tool at XXXX if you require support for ArcMap 9.3.

.. note::
   While the tool functions most efficiently with the advanced license (the old ArcInfo), it is possible to use other license versions.  Using a lesser license willr esult in some performance loss, but the final analysis remains identical.  The speed differential is most notable using the Distance Matrix tool as the advanced tool provides the Generate Near Table analysis tool.
   
Installing the Tool
-------------------

Adding the Tool to ArcMap
-------------------------
1. Launch ArcMap.
2. In the Main Menu bar select `Customize > Toolbar > LCC Analysis Tools`.
3. Ensure that a checkmark appears next to the `LCC Analysis Tools` entry in the menu.
4. The LCC toolbar should now appear in your ArcMap window.  This is a dockable toolbar, similar to the default toolbars you are familiar with.  It should appear each time you relaunch ArcMap until you manually close it.

.. image:: ../images/installation/img1.png

General Tool Descriptions
-------------------------
This section provides a brief overview of the purpose and functionality contained within each tool.

.. image:: ../images/installation/img2.png
    :align: center
	
	
======= ================================== 
Tool ID Tool Name 
1       Distance Matrix Tool                
2       Clustering Tool
3       Directional Distribution Tool
4       Trajectory Tool
5       Primary Impact Approximation Tool
======= ================================== 

Distance Matrix Tool
+++++++++++++++++++++++
**Description**: This tool computes the *k* nearest neighbors in either planar or geodesic space and stores the output as a table.

**Inputs**: A point shapefile or features class of digitized craters.

**Required Fields**: None

**Output**: A table storing the input FID, *k*-nearest neighbor FIDs, distances, and (x,y) coordinates of the neighbors.  The table will have a number of rows equal to n \* k, where *n* is the number of input points.

Clustering Tool
+++++++++++++++
**Description**: Using the nearest neighbor data stored in the distance matrix table, this tool analyzes the spatial distribution of the secondaries and locates clusters of craters.  It is possible to cluster the data heirarchally or using a density based scan.

**Inputs**: A point shapefile or features class of digitized craters and a distance matrix table with at least 1 nearest neighbor for each input point (*k* = 1 using the Distance Matrix Tool).

**Required Fields**: Distance table - `FID` and `IN_FID` // Point shapefile or featureclass - None

**Output**: A point shapefile or featureclass with clusters identified with a unique cluster ID.

Directional Distribution Tool
++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
**Description**: Using the output from the previous tool, the directional distribution tool generates a bounding ellipsoid around each cluster and identified both the length and directionality of the ellipoid's major-axis, as well as the ellipticity of the cluster.  

**Inputs**: A point shapefile or featureclass of clustered secondaries.

**Required Fields**: ?????

**Output**: A polyline shapefile showing either the bounding ellipsoid or the semi-major axis of the bounding ellipsoid.

Trajectory Tool
++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
**Description**: This tool extends the directional polyline, that is a best fit to the cluster, into a trajectory along which the material may have traveled.  These trajectories can extent up to 360 degrees from the origin (the bounding ellipsoid or semi-major polyline identified for each cluster).

.. note::
   It is possible to begin using the tool from this point if secondaries have been digitized, not as individual points, but as polylines of crater chains, i.e. the digitizer has visually identified clusters and draw a best fit polyline to that cluster.

**Inputs**: One ore more polyline shapefiles.

**Required Fields**: ??????

**Output**: A polyline shapefile or featureclass depicting the trajectories for clusters of secondaries.

Primary Impact Approximation Tool
++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
**Description**: This tool intersects the trajectories of secondaries with the goal of identifying the source point. 

**Inputs**: A polyline trajectory shapefile.

**Required Fields**: ????

**Output**: A point shapefile or featureclass with one or more primary impacts identified.