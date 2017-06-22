IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipList')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipList'
	DROP  Procedure  dbo.SystemForeignRelationshipList
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipList'
GO

/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipList
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

CREATE Procedure dbo.SystemForeignRelationshipList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SystemForeignRelationship'
)
AS
BEGIN

	SELECT	*
	 FROM	dbo.SystemForeignRelationship 
	 ORDER BY SystemForeignRelationshipId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO