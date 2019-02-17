SELECT p.PartId,
       p.[Description],
       ISNULL(pn.Quantity, 0) AS [Required],
       ISNULL(p.StockQty, 0) AS [In Stock],
       ISNULL(CASE
                  WHEN o.Delivered = 0
                  THEN op.Quantity
                  ELSE 0
              END, 0) AS Ordered
FROM Parts AS p
     JOIN PartsNeeded AS pn ON pn.PartId = p.PartId
     LEFT JOIN OrderParts AS op ON op.PartId = p.PartId
     JOIN Jobs AS j ON j.JobId = pn.JobId
     LEFT JOIN Orders AS o ON o.JobId = j.JobId
WHERE pn.Quantity > ISNULL(p.StockQty + CASE
                                            WHEN o.Delivered = 0
                                            THEN op.Quantity
                                            ELSE 0
                                        END, 0)
      AND j.Status != 'Finished'
ORDER BY p.PartId