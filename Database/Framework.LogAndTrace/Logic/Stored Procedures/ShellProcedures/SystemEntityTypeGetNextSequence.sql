IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeGetNextSequence')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeGetNextSequence'
	DROP  Procedure dbo.SystemEntityTypeGetNextSequence
END
GO

PRINT 'Creating Procedure SystemEntityTypeGetNextSequence'
GO

/******************************************************************************
**		File: 
**		Name: SystemEntityTypeGetNextSequence
**		Desc: Thisd procedure is to create next SystemEntityTypeId.
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
**		Date: 09-22-05
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				EntityName:
**		--------	--------			--------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE dbo.SystemEntityTypeGetNextSequence
(
		@SystemEntityTypeId		INT			= NULL
	,	@EntityName				VARCHAR(50) = NULL
	,	@NextValue				INT					OUTPUT
	,	@AuditId				INT			= NULL
)
AS
BEGIN	
EXEC TaskTimeTracker.dbo.SystemEntityTypeGetNextSequence NULL, @EntityName, @NextValue OUTPUT, @AuditId
	END
GO
/*
DECLARE @NextValue INT

EXECUTE dbo.SystemEntityTypeGetNextSequence 
			NULL
	,		'Person'
	,		@NextValue OUT

SELECT @NextValue


SELECT * FROM SystemEntityType

SELECT "@NextValue" = @NextValue
*/