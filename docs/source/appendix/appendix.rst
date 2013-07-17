.. appendix1

==========
Appendix
==========

Key Terms
-----------

.. _app-distance_matrix:

**Distance Matrix**: A dense matrix that stores the distance between each point observation and the *k*-nearest neighbors.  Because the matrix is dense it is computationally expensive to populate all distances and prohibitive to store the entire matrix, in memory, for a large number of observations.

.. _app-ellipticity:
   
**Ellipticity**: 'Flattening is a measure of the compression of a circle or sphere along a diameter to from an ellipse.' [wiki2013flattening]_  This is expressed as:

:math:`\frac{1}{f}`, where
   
:math:`f=\frac{a-b}{a}`, where
   
*a* is the semi-minor axis of the ellipsoid and *b* is the semi-major axis of the ellipsoid.

.. image:: ../images/appendix/ellipticity.png

.. _app-geodesic:

**Geodesic Distance Measurement**: The distance between two points along the spheroid or geoid.  This distance measure is more accurate than a planar (euclidean) measurement, but incurs additional computational cost.

.. _app-planar:

**Planar Distance Measurement**: The euclidean distance between two points.  This measurement assumes that the points are coincident to a single plane and is computationally efficient to compute.  The planar distance between two points on a spheroid is inaccurate and the level of inaccuracy increases with the distance between both points.

Citations:
----------
.. [wiki2013flattening] Wikipedia (2013). Flattening. https://en.wikipedia.org/wiki/Flattening