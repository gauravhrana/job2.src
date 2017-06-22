IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDetailList')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDetailList'
	DROP  Procedure  dbo.SuperKeyDetailList
END
GO

PRINT 'Creating Procedure SuperKeyDetailList'
GO

/******************************************************************************
**		File: 
**		Name: SuperKeyDetailList
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

CREATE Procedure dbo.SuperKeyDetailList
(
		@ApplicationId			INT
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKeyDetail'
)
AS
BEGIN

	SELECT	a.SuperKeyDetailId
		,	a.ApplicationId	
		,	a.EntityKey
		,	a.SuperKeyId	
		,	b.Name					AS	'SuperKey'		
	FROM		dbo.SuperKeyDetail	a
	INNER JOIN	dbo.SuperKey		b	ON	a.SuperKeyId	=	b.SuperKeyId
	WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY SuperKeyDetailId			ASC
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