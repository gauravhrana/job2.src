IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyList')
BEGIN
	PRINT 'Dropping Procedure SuperKeyList'
	DROP  Procedure  dbo.SuperKeyList
END
GO

PRINT 'Creating Procedure SuperKeyList'
GO

/******************************************************************************
**		File: 
**		Name: SuperKeyList
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

CREATE Procedure dbo.SuperKeyList
(
		@ApplicationId			INT
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKey'
)
AS
BEGIN

	SELECT	a.SuperKeyId
		,	a.ApplicationId					
		,	a.Name						
		,	a.Description					
		,	a.SortOrder
		,	a.SystemEntityTypeId
		,	a.ExpirationDate
	FROM	dbo.SuperKey a 
	WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY SuperKeyId			ASC
		,	 SortOrder			ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO