IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBProjectNameList')
BEGIN
	PRINT 'Dropping Procedure DBProjectNameList'
	DROP  Procedure  dbo.DBProjectNameList
END
GO

PRINT 'Creating Procedure DBProjectNameList'
GO

/******************************************************************************
**		File: 
**		Name: DBProjectNameList
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

CREATE Procedure dbo.DBProjectNameList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DBProjectName'
)
AS
BEGIN

	SELECT	a.DBProjectNameId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.[Description]	   
		,	a.SortOrder
	 FROM	dbo.DBProjectName a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.DBProjectNameId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO