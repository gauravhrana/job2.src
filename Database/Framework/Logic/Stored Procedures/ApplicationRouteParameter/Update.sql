IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRouteParameterUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationRouteParameterUpdate'
	DROP  Procedure  ApplicationRouteParameterUpdate
END
GO

PRINT 'Creating Procedure ApplicationRouteParameterUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRouteParameterUpdate
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
**     ----------							-----------
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

CREATE Procedure dbo.ApplicationRouteParameterUpdate
(
		@ApplicationRouteParameterId		INT		 
	,	@ApplicationRouteId					INT				 
	,	@ParameterName						VARCHAR(100)					
	,	@ParameterValue						VARCHAR(100)	
	,	@AuditId							INT					
	,	@AuditDate							DATETIME	= NULL
	,	@SystemEntityType					VARCHAR(50)	= 'ApplicationRouteParameter'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationRouteParameter 
	SET		ApplicationRouteId					=	@ApplicationRouteId
		,	ParameterName						=	@ParameterName	
		,	ParameterValue						=	@ParameterValue										
	WHERE	ApplicationRouteParameterId			=	@ApplicationRouteParameterId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationRouteParameterId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO