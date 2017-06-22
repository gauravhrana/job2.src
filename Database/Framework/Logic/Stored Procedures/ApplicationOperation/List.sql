IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationList')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationList'
	DROP  Procedure dbo.ApplicationOperationList
END
GO

PRINT 'Creating Procedure ApplicationOperationList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationOperationList
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
CREATE Procedure [dbo].[ApplicationOperationList]
(
		@AuditId				INT			
	,	@ApplicationId			INT					
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationOperation'
)
AS
BEGIN

	SELECT	a.ApplicationOperationId			
		,	a.Name									
		,	a.Description						
		,	a.SortOrder						
		,	a.ApplicationId	
		,	b.Name				
		,	a.SystemEntityTypeId				
		,	c.EntityName
		,	a.OperationValue			
	FROM       dbo.ApplicationOperation				a	
	INNER JOIN Application							b ON a.ApplicationId		= b.ApplicationId
	INNER JOIN Configuration.dbo.SystemEntityType	c ON a.SystemEntityTypeId	= c.SystemEntityTypeId
	WHERE	a.ApplicationId		=	@ApplicationId
	ORDER BY	a.SortOrder					ASC
			,	a.ApplicationOperationId	ASC
			,	a.ApplicationId				ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	
END
GO		
