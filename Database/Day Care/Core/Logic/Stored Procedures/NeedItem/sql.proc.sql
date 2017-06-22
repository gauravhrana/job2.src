-- =============================================
-- Script Template
-- =============================================
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedItemList')
BEGIN
	PRINT 'Dropping Procedure NeedItemList'
	DROP PROCEDURE NeedItemList
END
GO

PRINT 'Creating Procedure NeedItemList'
GO

/******************************************************************************
**		File: 
**		Name: NeedItemList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/


Create Procedure dbo.NeedItemList
(
	@NeedItemId	INT = NULL
	
)
AS
	SELECT	
	                        
	    NeedItemId	
	,	Name				
	,	Description		
	,	SortOrder			
	FROM	NeedItem
		

