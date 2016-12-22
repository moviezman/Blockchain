
SELECT GestemdOp, Count(GestemdOp)AS 'AantalGestemd' 
FROM UC 
WHERE stemmingsNaam = 'Stemming1' 
GROUP BY GestemdOp 
ORDER BY AantalGestemd DESC;


SELECT Project.Naam, Count(UC.GestemdOp)
From UC join Project on UC.StemmingsNaam = Project.StemmingsNaam
where Project.StemmingsNaam= 'Stemming1' 
GROUP BY Project.Naam;