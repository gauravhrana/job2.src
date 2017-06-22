IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginHistoryDetails')
BEGIN
  PRINT 'Dropping Procedure UserLoginHistoryDetails'
  DROP  Procedure UserLoginHistoryDetails
END

GO

PRINT 'Creating Procedure UserLoginHistoryDetails'
GO


/******************************************************************************
**		File: 
**		Name: UserLoginHistoryDetails
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				RecordDate:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.UserLoginHistoryDetails
(
		@UserLoginHistoryId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'UserLoginHistory'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@UserLoginHistoryId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.UserLoginHistoryId 			
		,	a.ApplicationId as 'ApplicationId'
		,	a.UserName
		,	a.UserId
		,	a.URL
		,	a.ServerName
	   
	   ,	a.DateVisited
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM		dbo.UserLoginHistory		a
	
	WHERE	a.UserLoginHistoryIdId = @UserLoginHistoryIdId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLoginHistory'
		,	@EntityKey				= @UserLoginHistoryId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   