IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ConnectionStringXApplicationSearch')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringXApplicationSearch'
	DROP Procedure ConnectionStringXApplicationSearch
END
GO

PRINT 'Creating Procedure ConnectionStringXApplicationSearch'
GO

/******************************************************************************
**		File: 
**		Name: ConnectionStringXApplicationSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC ConnectionStringXApplicationSearch NULL	, NULL	, NULL
			EXEC ConnectionStringXApplicationSearch NULL	, 'K'	, NULL
			EXEC ConnectionStringXApplicationSearch 1		, 'K'	, NULL
			EXEC ConnectionStringXApplicationSearch 1		, NULL	, NULL
			EXEC ConnectionStringXApplicationSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure ConnectionStringXApplicationSearch
(
		@ConnectionStringXApplicationId		INT				= NULL
	,	@ApplicationId						INT				= NULL	
	,	@ConnectionStringId					INT				= NULL	
	,	@AuditId							INT						
	,	@AuditDate							DATETIME		= NULL
	,	@SystemEntityType					VARCHAR(50)		= 'ConnectionStringXApplication' 
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON	

	SELECT	a.ConnectionStringXApplicationId	
		,	a.ApplicationId	
		,	a.ApplicationId						
		,	a.ConnectionStringId								
		,	b.Name		AS	'Application'			
		,	c.Name		AS	'ConnectionString'
	FROM		dbo.ConnectionStringXApplication					a
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application		b	ON	a.ApplicationId	=	b.ApplicationId
	INNER JOIN	dbo.ConnectionString								c	ON	a.ConnectionStringId	=	c.ConnectionStringId	
	WHERE   a.ConnectionStringXApplicationId	= ISNULL(@ConnectionStringXApplicationId, a.ConnectionStringXApplicationId )
	AND		a.ApplicationId						= ISNULL(@ApplicationId, a.ApplicationId )
	AND		a.ConnectionStringId				= ISNULL(@ConnectionStringId, a.ConnectionStringId )
	ORDER BY a.ConnectionStringXApplicationId	ASC
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ConnectionStringXApplicationId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

