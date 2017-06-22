IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RenumberRunDetailList')
BEGIN
	PRINT 'Dropping Procedure RenumberRunDetailList'
	DROP  Procedure  dbo.RenumberRunDetailList
END
GO

PRINT 'Creating Procedure RenumberRunDetailList'
GO

/******************************************************************************
**		File: 
**		Name: RenumberRunDetailList
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.RenumberRunDetailList
(
	@SystemEntityType		VARCHAR(50)	= 'RenumberRunDetail'
)
AS
BEGIN

	SELECT  RenumberRunDetailId
		,	RenumberRunId														
		,	EntityName					
		,	FKEntityId
		,	OldId
		,	NewId		  		
				
	FROM	dbo.RenumberRunDetail 
	ORDER BY RenumberRunDetailId			ASC

	

END
GO