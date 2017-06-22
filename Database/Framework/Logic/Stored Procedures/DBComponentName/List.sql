IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBComponentNameList')
BEGIN
	PRINT 'Dropping Procedure DBComponentNameList'
	DROP  Procedure  dbo.DBComponentNameList
END
GO

PRINT 'Creating Procedure DBComponentNameList'
GO

/******************************************************************************
**		File: 
**		Name: DBComponentNameList
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.DBComponentNameList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DBComponentName'
)
AS
BEGIN

	SELECT	a.DBComponentNameId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.[Description]	   
		,	a.SortOrder
	 FROM	dbo.DBComponentName a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.DBComponentNameId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO