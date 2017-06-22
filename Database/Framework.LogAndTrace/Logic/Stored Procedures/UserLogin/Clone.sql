IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginClone')
BEGIN
	PRINT 'Dropping Procedure UserLoginClone'
	DROP  Procedure UserLoginClone
END
GO

PRINT 'Creating Procedure UserLoginClone'
GO

/*********************************************************************************************
**		File: 
**		Name: UserLoginClone
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				RecordDate:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.UserLoginClone
(
		@UserLoginId			INT				= NULL 	OUTPUT	
	,   @ApplicationId			INT				= NULL	
	,	@UserName				VARCHAR(50)						
	,	@UserLoginStatusId		INT										
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'UserLogin'
)
AS
BEGIN

	DECLARE @RecordDate AS DECIMAL
	SELECT @RecordDate = CAST( CONVERT(VARCHAR(8), GETDATE(), 112) + REPLACE(CONVERT(varchar(5), GETDATE(), 114), ':', '') AS DECIMAL)		
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@UserLoginStatusId		=	UserLoginStatusId
		,	@UserName				=	UserName				
	FROM	dbo.UserLogin
	WHERE   UserLoginId				=	@UserLoginId
	ORDER BY UserLoginId

	EXEC dbo.UserLoginInsert 
			@UserLoginId			=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@UserName				=	@UserName
		,	@RecordDate				=	@RecordDate
		,	@UserLoginStatusId		=	@UserLoginStatusId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLogin'
		,	@EntityKey				= @UserLoginId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
