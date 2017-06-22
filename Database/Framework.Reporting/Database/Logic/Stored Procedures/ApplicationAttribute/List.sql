IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationAttributeList')
BEGIN
	PRINT 'Dropping Procedure ApplicationAttributeList'
	DROP  Procedure  dbo.ApplicationAttributeList
END
GO

PRINT 'Creating Procedure ApplicationAttributeList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationAttributeList
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

CREATE Procedure dbo.ApplicationAttributeList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationAttribute'
)
AS
BEGIN

	SELECT	a.ApplicationId   
		,	a.Name		  	
		,	a.RenderApplicationFilter	   
		
	 FROM	dbo.ApplicationAttribute a inner join Application b on a.ApplicationId = b.ApplicationId
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.ApplicationAttributeId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO