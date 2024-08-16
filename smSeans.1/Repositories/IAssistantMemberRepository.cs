using System;
using System.Threading.Tasks;

public interface IAssistantMemberRepository
{
    Task<AssistantMember> GetAssistantMemberByIdAsync(int id);
    Task<int> CreateAssistantMemberAsync(AssistantMember assistantMember);
    Task<bool>   UpdateAssistantMemberAsync(AssistantMember assistantMember);
    Task<bool> DeleteAssistantMemberAsync(int id);
}
