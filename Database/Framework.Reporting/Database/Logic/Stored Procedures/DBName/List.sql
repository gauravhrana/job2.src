IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBNameList')
BEGIN
	PRINT 'Dropping Procedure DBNameList'
	DROP  Procedure  dbo.DBNameList
END
GO

PRINT 'Creating Procedure DBNameList'
GO

/******************************************************************************
**		File: 
**		Name: DBNameList
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

CREATE Procedure dbo.DBNameList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DBName'
)
AS
BEGIN

	SELECT	a.DBNameId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.[Description]	   
		,	a.SortOrder
	 FROM	dbo.DBName a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.DBNameId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO