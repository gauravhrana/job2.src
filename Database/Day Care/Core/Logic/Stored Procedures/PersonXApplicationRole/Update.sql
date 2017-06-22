IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonXApplicationRoleUpdate')
	BEGIN
		PRINT 'Dropping Procedure PersonXApplicationRoleUpdate'
		DROP  Procedure  PersonXApplicationRoleUpdate
	END

GO

PRINT 'Creating Procedure PersonXApplicationRoleUpdate'
GO


/******************************************************************************
**		File: 
**		Name: PersonXApplicationRoleUpdate
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

CREATE Procedure dbo.PersonXApplicationRoleUpdate
(
	@Person_X_ApplicationRoleId		INT		,
	@PersonId						INT		,	
	@ApplicationRoleId				INT
)
AS
	UPDATE	Person_X_ApplicationRole 
	SET		PersonId			=	@PersonId				,			
			ApplicationRoleId	=	@ApplicationRoleId							
	WHERE	Person_X_ApplicationRoleId	=	@Person_X_ApplicationRoleId	

GO

