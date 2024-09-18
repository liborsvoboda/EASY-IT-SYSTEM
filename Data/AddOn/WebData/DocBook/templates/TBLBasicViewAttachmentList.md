IF OBJECT_ID('[dbo].[BasicViewAttachmentList]') IS NOT NULL 
 DROP  VIEW      [dbo].[BasicViewAttachmentList] 
 GO
 CREATE VIEW dbo.BasicViewAttachmentList
AS
SELECT        TOP (100) PERCENT AL.Id, AL.FileName, IL.PartNumber, AL.TimeStamp
FROM            dbo.BasicAttachmentList AS AL LEFT OUTER JOIN
                         dbo.BasicItemList AS IL ON AL.ParentId = IL.Id AND AL.ParentType = 'ITEM'
ORDER BY AL.FileName

GO
