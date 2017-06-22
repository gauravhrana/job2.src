IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TraceDoesExist')
BEGIN
	PRINT 'Dropping Procedure TraceDoesExist'
	DROP  Procedure  TraceDoesExist
END
GO

PRINT 'Creating Procedure TraceDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TraceDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.TraceDoesExist
(
		@TraceId			INT				= NULL
	,	@Name						VARCHAR(50)		
	,	@ApplicationId				INT	
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'Trace'
)
AS
BEGIN

	SELECT	a.*
	FROM	dbo.Trace a
	WHERE	a.Name			=	@Name
	AND		a.ApplicationId	=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Trace'
		,	@EntityKey				= @TraceId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO

