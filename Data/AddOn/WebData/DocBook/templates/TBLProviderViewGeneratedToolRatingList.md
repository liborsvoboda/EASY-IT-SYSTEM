IF OBJECT_ID('[dbo].[ProviderViewGeneratedToolRatingList]') IS NOT NULL 
 DROP  VIEW      [dbo].[ProviderViewGeneratedToolRatingList] 
 GO
 CREATE VIEW dbo.ProviderViewGeneratedToolRatingList
AS
SELECT        Name, AVG(Rating) AS Rating, COUNT(Id) AS CountRating,
                             (SELECT        COUNT(Id) AS Expr1
                               FROM            dbo.ProviderGeneratedToolList AS gt
                               WHERE        (Name = g.Name)) AS TotalCount
FROM            dbo.ProviderGeneratedToolList AS g
WHERE        (Rating IS NOT NULL)
GROUP BY Name

GO
