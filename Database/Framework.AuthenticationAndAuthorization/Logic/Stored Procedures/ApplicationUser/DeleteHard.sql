IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserDeleteHard'
	DROP  Procedure ApplicationUserDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationUserDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: ApplicationUserDeleteHard
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

CREATE Procedure dbo.ApplicationUserDeleteHard
(
		@KeyId 				INT					
	,	@KeyType			VARCHAR(50)			
	,	@AuditId			INT					
	,	@AuditDate			DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'ApplicationUser'			
)
AS
BEGIN

	IF @KeyType = 'ApplicationUserId'
		BEGIN 	

			EXEC	dbo.SystemDevNumbersDeleteHard
					@KeyId		=	@KeyId		 
				,	@KeyType	=	'ApplicationUserId'	
				,	@AuditId	=	@AuditId

			EXEC	dbo.BatchFileDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'ApplicationUserId'
				,	@AuditId	=	@AuditId

			EXEC	dbo.ScheduleItemDeleteHard 
					@KeyId		=	@KeyId
				,	@KeyType	=	'ApplicationUserId'
				,	@AuditId	=	@AuditId
			DELETE	dbo.ApplicationUser
			WHERE	ApplicationUserId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
