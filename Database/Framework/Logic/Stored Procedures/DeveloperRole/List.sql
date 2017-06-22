IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeveloperRoleList')
BEGIN
	PRINT 'Dropping Procedure DeveloperRoleList'
	DROP  Procedure  dbo.DeveloperRoleList
END
GO

PRINT 'Creating Procedure DeveloperRoleList'
GO

/******************************************************************************
**		File: 
**		Name: DeveloperRoleList
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

CREATE Procedure dbo.DeveloperRoleList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DeveloperRole'
)
AS
BEGIN

	SELECT	a.DeveloperRoleId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.Description	   
		,	a.SortOrder
	 FROM	dbo.DeveloperRole a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.DeveloperRoleId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO